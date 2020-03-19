--AUTHOR : 梁振东
--DATE : 9/26/2019 1:43:49 PM
--DESC : ****
--面向对象
local M = {};
M.__index = M;
M.ModuleName = "LuaOret";
M.a = 10;
M.b = 20;
function M : New(o)
    o = o or {};
    setmetatable(o, self)
    return o;
end
function M : AddTest()
    self.a = self.a + 1;
    self.b = self.b + 1;
end
function M : PrintMember()
    print(self.a);
    print(self.b);
end
return M;