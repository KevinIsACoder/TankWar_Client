--AUTHOR : 梁振东
--DATE : 9/26/2019 10:47:01 AM
--DESC : ****
--require只会load一次，不会重复load
--The simplest way to create a module in Lua is really simple: we create a table, put all functions we want to export inside it, 
--and return this table. Listing 15.1 illustrates this approach. 
--Note how we define inv as a private function simply by declaring it local to the chunk.

local M = {};
M.moduleName = "";
M.__index = M;
--if a module does not return a value, require will return the current value of package.loaded[modname] (if it is not nil). 
--Anyway, I still prefer to write the final return, because it looks clearer.
local N = {};
package.loaded[...] = N;
return M;

