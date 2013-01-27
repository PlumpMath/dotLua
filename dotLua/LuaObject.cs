﻿using System;
using System.Dynamic;

namespace dotLua
{
    public class LuaObject : DynamicObject
    {
        private readonly Lua _lua;
        private readonly GetMemberBinder _binder;

        public LuaObject(Lua lua, GetMemberBinder binder)
        {
            _lua = lua;
            _binder = binder;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            result = _lua.Invoke(_binder.Name, args);
            return true;
        }
    }
}