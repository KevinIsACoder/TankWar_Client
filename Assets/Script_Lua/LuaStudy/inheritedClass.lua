--AUTHOR : 梁振东
--DATE : 9/26/2019 2:24:44 PM
--DESC : ****
local M = {};
M.__index = M;
M.a = 1;
M.b = 2;
setmetatable(M, require("LuaStudy.LuaOret"));
function M:New(o)
    o = o or {};
    setmetatable(o, M)
    return o;
end
function M : AddTest()
    getmetatable(M).AddTest(self);
end
function M : PrintMember()
    getmetatable(M).PrintMember(self);
end
-- for multiInherited
function M:MultiInherited()
    print("Its Multy Method");
end
return M;