--AUTHOR : 梁振东
--DATE : 9/24/2019 1:27:12 PM
--DESC : ****
--The table type implements associative arrays. 
--An associative array is an array that can be indexed not only with numbers, but also with strings or any other value of the language, except nil.
local M = {};
M.__index = M;
function M:Init()
    self.t = {1, 2, 3};
    for i = 1, #self.t do
        print(self.t[i]);
    end
end

return M;