INCLUDE Globals.ink
-> main.knowledge1
VAR choice = 0
=== main ===
->DONE
=knowledge1
Do you really know me?
Knowledge Check 1
    + [Choice 1]
~ choice  = 1
    + [Choice 2]
~ choice  = 2
    + [Choice 3]
~ choice  = 3
    + [Choice 4]
~ choice  = 4
-{choice==npc1Knowledge1: 
    Alright.
    ->knowledge2
    
    }
-Wrong Choice Fraudster!
->DONE
=knowledge2
Do you really know me?
Knowledge Check 2
    + [Choice 1]
~ choice  = 1
    + [Choice 2]
~ choice  = 2
    + [Choice 3]
~ choice  = 3
    + [Choice 4]
~ choice  = 4
-{choice==npc1Knowledge2: 
    Your getting good at this. 
    ->knowledge3

    }
-Wrong Choice Fraudster!
->DONE
=knowledge3
Do you really know me?
Knowledge Check 3
    + [Choice 1]
~ choice  = 1
    + [Choice 2]
~ choice  = 2
    + [Choice 3]
~ choice  = 3
    + [Choice 4]
~ choice  = 4
-{choice==npc1Knowledge3: 
    O_O 
    ->knowledge4
    
    }
-Wrong Choice Fraudster!
->DONE
=knowledge4
Do you really know me?
Knowledge Check 4
    + [Choice 1]
~ choice  = 1
    + [Choice 2]
~ choice  = 2
    + [Choice 3]
~ choice  = 3
    + [Choice 4]
~ choice  = 4
-{choice==npc1Knowledge4: 
I can see you are real. 
~ npc1Trust = true
->DONE
    }
-Wrong Choice Fraudster!
->DONE
->DONE
-> END
