-- region *.lua
-- Date
-- 此文件由[BabeLua]插件自动生成

local moduleName = ...;
local M = {};
M.__index = M;
function Start()
    print("Lua Main Begin Start");
end

function Exit()
    print("Lua Main Exit");
end

function Bind(func,obj)
    return function(...)
        return func(obj,...);
    end
end
local util = require "xlua.util";
local Yield = util.async_to_sync(function(yield,callback)
   LZDFramework.LuaManager:Yield(yield,callback);
end)
function WaitForEndOfFrame() Yield(CS.UnityEngine.WaitForEndOfFrame()); end
function WaitForFixedUpdate() Yield(CS.UnityEngine.WaitForFixedUpdate()); end
function WaitForSeconds(sec) Yield(CS.UnityEngine.WaitForSeconds(sec)); end
return M;
-- endregion
