--AUTHOR : 梁振东
--DATE : 9/26/2019 1:43:49 PM
--DESC : ****
--Lua 携程
-- coroutine.yield()会将额外参数返回给resume
--例：     co = coroutine.create（函数（a，b）
--coroutine.yield（a + b，a-b）
---结束）
--print（coroutine.resume（co，20，10））->真30 10
-- 对称地，yield返回传递给相应参数的所有其他参数resume：
--     co = coroutine.create（函数（）
--            打印（“ co”，coroutine.yield（））
--          结束）
--     coroutine.resume（co）
--     coroutine.resume（co，4，5）-> co 4 5
-- lua提供了我所说的不对称协程。这意味着它具有暂停执行协程的功能和恢复暂停的协程的功能。
-- 其他一些语言提供对称协程，其中只有一种功能可以将控制权从任何协程转移到另一协程。
local M = {};
M.__index = M;
M.coroutine = nil;
function M:New()
    o = o or {};
    setmetatable(o, M);
    return o;
end
function foo(a)
    print("f00", a);
    return coroutine.yield(2 * 1);
end
function M:Create()  --创建一个，开始状态是挂起状态
    local co = coroutine.create(function(a, b)
        print("cor-body", a, b)
        local r = foo(a + 1);
        print("cor-body", r);
        local r, s = coroutine.yield(a + b, a - b)
        print("cor-body", r, s);
        return b, "end";
    end)
    print("main", coroutine.resume(co, 1, 10))
    print("main", coroutine.resume(co, "r"))
    print("main", coroutine.resume(co, "x", "y"))
    print("main", coroutine.resume(co, "x", "y"))
end
return M;
