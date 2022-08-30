# SnelToetsenSjezer
Application to practise usage of Hot Keys  
  
### Current state:  
Its working, but part of the functionality isnt there yet/is incomplete, most important todo is the need to track time, second most important is to use forms instead of debug output for user-feedback, after having taken care of those two i wanna re-consider some of the code because the whole thing got a bit messy in my quest to get it functional & i already figured out a hotkey combination that the program doesnt support but really should.  
  
### TODO:  
- [-] Implement a timer so we can figure out how long somebody took  
- [-] Implement a gameover screen/form  
- [-] Turn 'Main menu' into some actual basic menu (show list of categories with checkboxes & input box for how many questions, with check on not being able to set amount of questions higher than max available questions)  
- [-] Refactor/cleanup code a bit  
- [-] Change how the repeating of questions works, while it currently works i didnt really follow the assignment close enough wich has lead to less than optimal user experience (once you start working on previously failed questions, if you fail one of those it just repeats right away, wich is odd)  
- [-] Come up with a way to have both 'AllAtOnce' and 'Sequential' in one hotkey, currently (with the provided examples) we do one or the other, but for example if you wanna have "make a comment in Visual Studio" as a question you cant set the solution to be Ctrl+K,Ctrl+C  
- [-] Most of the logic is currently tied to keydown event, that works fine but doesnt leave any room for user-feedback, if they make a mistake or complete the question the application just instantly jumps to the next question, would be better if there was some sort of pause, feedback & then continue to next question. Also need this change to be able to even show the expected answer  

***([-] is not done, [X] is done)***  

---

### Extended TODO:  
- [-] A checkbox in main menu (default: checked) if you should be allowed to retry failed questions (maybe also a input box for how many retries, default: -1 for forever?) because it makes a lot more sense to me to 'accept' faulty awnsers (and then show those as failed in gameover screen) instead of giving the user unlimited retries for those  
- [-] Try once more to add support for LCtrl/RCtrl/LShift/RShift etc (tried this early on and it was just rabbithole after rabbithole, appearantly this isnt a easy thing to achieve in C#..?! thats not acceptible to me though, i should just be able to read out actual keys instead of some interpertation)  
- [-] Go over all special characters/keys of keyboard to make sure they are functional, its already being annoying with things like a backslash being called Oem5 or how Alt is sometimes detected as Alt, sometimes as AltKey, sometimes as Menu, etc. (seriously what even, shame on you C# for not being consistent wich such a basic thing)  

***([-] is not done, [X] is done)***  
