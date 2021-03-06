-- region *.lua
-- Date
-- 此文件由[BabeLua]插件自动生成

local moduleName = ...;
local M = {};
M.__index = M;
function Start()
    print("Lua Main Begin Start");
   -- require("LuaStudy.LuaTable.TableStudy").Init();
   local o = require("LuaStudy.LuaOret") : New();
   o:AddTest();
   o:PrintMember(); --值改变
   require("LuaStudy.LuaOret"):PrintMember(); --值不变
   
   --继承测试
   local t = require("LuaStudy.inheritedClass"):New();
   t:AddTest();
   t:PrintMember();
   local z = require("LuaStudy.inheritedClass"):New();
   z:AddTest();
   z:PrintMember();

   local multi = require("LuaStudy.MultiInheritedClass"):New()
   multi:AddTest();
   multi:PrintMember();
   multi:MultiInherited();
   --协程测试
   local cor = require("LuaStudy.LuaCoroutine"):New()
   cor:Create() 
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
local luatable = require "LuaStudy.LuaTable.TableStudy";
local Yield = util.async_to_sync(function(yield,callback)
   LZDFramework.LuaManager:Yield(yield,callback);
end)
function WaitForEndOfFrame() Yield(CS.UnityEngine.WaitForEndOfFrame()); end
function WaitForFixedUpdate() Yield(CS.UnityEngine.WaitForFixedUpdate()); end
function WaitForSeconds(sec) Yield(CS.UnityEngine.WaitForSeconds(sec)); end
function status()  end
return M;
-- endregion
