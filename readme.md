Journal
=============

2023-10-27
---------------

#### 4102 Unit Test ParkingMachine - Klassen Bank
After trying to debug and find an issue in the program, I came across a mistake I had made previously regarding 
adding each bank account to a dictionary. Instead of using the bank account's accountNumber, I was trying to use its pin. 
This was causing issues, but fixing it did not fix all issues. With some help, another problem regarding AccountNrGenerator
was terminated, but alas, the program is still not back to a functional state.

Some time later I've now found one part of the problem. I was modifying a function in the class Bank, and I was fairly certain
that the changes would make the tests work, but they did not. They, in fact, did not change regardless of how I seemed to change
the function. This is, as I realized eventually, because there were two identical functions, one of them taking an extra 
parameter. The function I was modifying takes two parameters, but the function call that the test checked calls said function
with one argument. In hindsight, I should've realized far sooner that I was modifying the wrong function, but in the end,
I was making correct changes at least. So the test passes now, but the program (some other tests) still doesn't run. And this,
I do not know why.

Some time later again, I've now found another problem. Rather, I found the problem some time ago and now know what is wrong.
Earlier today, before changing some things with AccountNrGenerator, as mentioned previously, I incremented a static variable
every time the constructor for AccountNrGenerator was called. Moving the incrementer to a method instead of the constructor 
means that I cannot keep the variable as static. The tests do not pass if I do, so I'll have to change this. What I do not
understand though, is why the program refuses to run. I can run every single test individually, but not together. If I try to, 
it says that there are "build errors". Building the solution works fine, so I really do not know what is wrong. At least
the test that started all this debugging sort of works. It doesn't yet pass, but now the only issue is that the numbers are
wrong, no exceptions are thrown.

Now the aforementioned test passes, along with several others. The program as a whole still seems somewhat unstable, as I cannot
run all tests at once. Regardless, I'll try solving all tests first and then see what happens.

And now all the tests are working, with one exception. The exected value of `bank.GetBalance(accountNrTo, "11");` is 0, but
this test confuses me somewhat. Earlier in the code, this is written: `String accountNrTo = bank.AddAccount("22", 4000);`. 
This means that the account has a balance of 4000. The test essentially expects the balance of the account to be reset if
the incorrect pin is entered during a transaction. I'm unsure of whether this is the intended result or not, so for now, I'll 
save the test for later.

2023-11-07 
------------

#### 4102 Unit Test ParkingMachine - Klassen Bank
Now all the tests run successfully, but I still cannot "Run All Tests In View". Running all the tests at once with "Run" works,
so I don't know what is wrong here. 