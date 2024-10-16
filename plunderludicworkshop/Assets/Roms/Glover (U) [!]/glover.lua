
function main()
    print("running main");
    while true do
        -- print("running a frame");
        emu.yield() -- frameadvance() also works
        --local x = unityhawk.callmethod("DoSomething", "hello from lua");
        --local x = unityhawk.callmethod("DoSomethingElse", "hello from lua");
        --memory.write_u32_le(0x1E6704,10);
        local n=memory.read_u32_be(0x1E6704, "RDRAM");
        print(n)
        local x=unityhawk.callmethod("LuaCallback",""..n);
        -- print(x);
    end
end
-- 1E6704

main()