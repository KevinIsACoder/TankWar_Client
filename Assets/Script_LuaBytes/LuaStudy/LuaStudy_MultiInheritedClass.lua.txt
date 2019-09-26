--AUTHOR : 梁振东
--DATE : 9/26/2019 5:54:10 PM
--DESC : ****
local M = {};
M.__index = M;
M.a = 5
M.b = 6
setmetatable(M, require("LuaStudy.inheritedClass"))
function M:New(o)
    o = o or {};
    setmetatable(o, M);
    return o
end

return M;