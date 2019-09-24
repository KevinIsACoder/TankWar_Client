--AUTHOR : 梁振东
--DATE : 9/24/2019 1:27:12 PM
--DESC : ****
--The table type implements associative arrays. 
--An associative array is an array that can be indexed not only with numbers, but also with strings or any other value of the language, except nil.
-- 可见：ipairs并不会输出table中存储的键值对，会跳过键值对，然后顺序输出table中的值，遇到nil则会停止。
--而pairs会输出table中的键和键值对，先顺序输出值，再乱序（键的哈希值）输出键值对。
--table在存储值的时候是按照顺序的，但是在存储键值对的时候是按照键的哈希值存储的，并不会按照键的字母顺序或是数字顺序存储。
local M = {};
M.__index = M;
function M:Init()
    self.t = {2 ,nil, 3}; -- 2,2,1
   for i = 1, #self.t do
       print(self.t[i]); --2, nil, 3
   end
    for k, v in pairs(self.t) do
        print(v);  -- 2, 3,
    end
    -- for k, v in ipairs(self.t) do
    --     print(v); --2
    -- end
end
    --闭合函数
function NewCounter()
    local i = 0;
    return function()
        i = i + 1;
        return i;
    end
end
--异常的抛出和捕获
local state, err = pcall(function() error({code = 120}) end)
print(err.code);
--A useful facility in Lua is that a pair resume–yield can exchange data. The first resume, which has no corresponding yield waiting for it, 
--passes its extra arguments as arguments to the coroutine main function:
local co = coroutine.create(function(a, b, c) print("co", a, b, c + 2) end)
coroutine.resume(co, 1, 2, 3);

local co = coroutine.create(function(a, b) coroutine.yield(a + b, a - b) end)
coroutine.resume(co, 10, 20);
return M;