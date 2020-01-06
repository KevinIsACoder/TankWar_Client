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
    self.t = {1 ,nil, 3};
    self.t[5] = 5;
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
    --闭合函数： 当一个函数写在另一个函数的内部， 而这个内部函数可以访问外部函数的局部变量， 这个被访问的局部变量既不是全局的，也不是局部的， 称为非局部变量
function NewCounter()
    local i = 0;
    return function()
        i = i + 1;
        return i;
    end
end
local counter = NewCounter()
print(counter()) -- 1; 
print(counter()) --2;   
function f1(n)
    local function f2()
        print(n)
    end
    n = n + 10;
    return f2;
end
g1 = f1(1979)
g1();
-- g1()打印出来的是1989，原因是打印的是upvalue的值。

-- upvalue实际是局部变量，而局部变量是保存在函数堆栈框架上的，所以只要upvalue还没有离开自己的作用域，它就一直生存在函数堆栈上。这种情况下，闭包将通过指向堆栈上的upvalue的引用来访问它们，一旦upvalue即将离开自己的作用域，在从堆栈上消除之前，闭包就会为它分配空间并保存当前的值，以后便可通过指向新分配空间的引用来访问该upvalue。当执行到f1(1979)的n　=　n　+　10时，闭包已经创建了，但是变量n并没有离开作用域，所以闭包仍然引用堆栈上的n，
-- 当return　f2完成时，n即将结束生命，此时闭包便将变量n(已经是1989了)复制到自己管理的空间中以便将来访问。 
--多个闭包可以共享一个upvalue值
--异常的抛出和捕获
local state, err = pcall(function() error({code = 120}) end)
print(err.code);
--A useful facility in Lua is that a pair resume–yield can exchange data. The first resume, which has no corresponding yield waiting for it, 
--passes its extra arguments as arguments to the coroutine main function:
local co = coroutine.create(function(a, b, c) print("co", a, b, c + 2) end)
coroutine.resume(co, 1, 2, 3);

local co = coroutine.create(function(a, b) coroutine.yield(a + b, a - b) end)
coroutine.resume(co, 10, 20);

--Lua携程的状态， normal, substand, running, dead , 不能从外部终止， c#可以用stop终止
--携程状态及yield与resume交互

-- 协程1
co1 = coroutine.create(function ( a )
    print("co1 arg is :"..a)
    status()
 
	-- 唤醒协程2
	local stat,rere = coroutine.resume(co2,"2")
    print("111 co2 resume's return is "..rere)
    status()
 
	-- 再次唤醒协程2
	local stat2,rere2 = coroutine.resume(co2,"4")
    print("222 co2 resume's return is "..rere2)
    local arg = coroutine.yield("6")
end)

-- 协程2
co2 = coroutine.create(function ( a )
    print("co2 arg is :"..a)
    status()
    local rey = coroutine.yield("3")
    print("co2 yeild's return is " .. rey)
    status()
    coroutine.yield("5")
end)

-- co1 arg is :main thread arg				-- 开始执行协程1，第8行
-- co1's status :running ,co2's status: suspended		-- 协程1中，第9行，调用了status()函数
-- co2 arg is :2						-- 协程1中，第12行，调用了resume()，唤醒协程2，调用到24行
-- co1's status :normal ,co2's status: running		-- 注意：此时协程1处于normal状态，协程2处于running状态
-- 111 co2 resume's return is 3				-- 由于26行，协程2执行了yiled(),协程挂起，参数“3”被返回到协程1，赋值给了12行中resume()的第二个参数，在13行进行此打印
-- co1's status :running ,co2's status: suspended		-- 此时协程1被唤醒，处于running状态，协程2处于挂起状态
-- co2 yeild's return is 4					-- 由于17行，协程2被再次唤醒，由于不是第一次调用resume()，参数“4”被赋值给上次26行的yiled()的返回值，打印出来，此时是27行的
-- co1's status :normal ,co2's status: running		-- 同第一次，此时协程1处于normal状态，协程2处于running状态
-- 222 co2 resume's return is 5				-- 由于第29行执行yield完毕，参数5作为17行的resume()的返回值，在18行进行了打印，注意此时协程2仍未结束，处于挂起状态
-- co1's status :suspended ,co2's status: suspended	-- 由于第19行，执行了yield()，参数“6”被返回给33行的mainre，注意：此时协程1挂起，同样也未执行完

--主线程执行协程co1,传入字符串“main thread arg”
--print("last return is "..mainre)
return M;