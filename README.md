dotLua
======
dotLua is a Lua 5.2 binder for C# utilizing C# 4.0 dynamic class, by interop with Lua native C DLL. 

Example
======
test.lua:

	function say_hi(...)
		if ... ~= nil then
			message = "Hi"
			for _, v in pairs({...}) do
				message = message .. ", " .. v
			end
			print(message)
		else
			print("Hi from Lua")
		end
	end

	function raise(message)
		error(message)
	end

	GlobalNumber = 3
	GlobalString = "Good luck~"
	GlobalBoolean = true
	
	function return_0_str()
		return 0, "Hi~"
	end

In C#:

	using (dynamic lua = new Lua())
	{
		lua.Do("test.lua");
		lua.say_hi();
		lua.say_hi("dotLua");
		lua.say_hi(3.1415926);
		lua.say_hi(3.1415926, "dotLua", "Sunday");

		Console.WriteLine("Global Value: {0}", lua.GlobalNumber);
		Console.WriteLine("Global Value: {0}", lua.GlobalString);
		
		IList<dynamic> result0str = lua.return_0_str();
		Assert.AreEqual(2, result0str.Count);
		Assert.AreEqual(0, result0str[0]);
		Assert.AreEqual("Hi~", result0str[1]);

		try
		{
			lua.raise("Boom!");
			Console.WriteLine("Error: Lua error should be converted to an exception!");
		}
		catch (InvocationException e)
		{
			Console.WriteLine(e.Message);
		}
	}
	
Features
======
* Calling a static function defined in a Lua script and acquiring results.
* Getting a global number/string/boolean field defined in a Lua script.

TO DO
======
* Getting other Lua primitive types of global values in a Lua script.
* Calling methods of a Lua object. (Defined in metatable)
* Register a C# delegate to be called from a Lua script.
* Other Lua feature support.

License
======
This program is free software. It comes without any warranty, to
the extent permitted by applicable law. You can redistribute it
and/or modify it under the terms of the Do What The Fuck You Want
To Public License, Version 2, as published by Sam Hocevar. See
http://sam.zoy.org/wtfpl/COPYING for more details.
