--AUTHOR : 梁振东
--DATE : 9/25/2019 7:43:49 PM
--DESC : ****
--index:查寻
--—__newindex 修改
local M = {};
M.__index = M;
property = {1, 2, 3};
local mt = {};
function new(o)
    setmetatable(o, mt);
    return o;
end
mt.__index = function(_, v) print(property[v]) end

--serDefaultvalue
local mt = {__index = function(t) return t.___ end}
function setDefault(t,d)
    t.___ = d;
    setmetatable(t, mt)
end
local key = {};
local mt = {__index = function() return t[key] end}

return M;