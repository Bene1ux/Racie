# LVL Guide

PoE leveling guide plugin for PoeHelper

##### Example Guide
```
[QS a1q1 3]
Talk to Tarkleigh [QT a1q1]

Get The Coast WP [P The Coast]
[G The Mud Flats]
[QS a1q4 2]
Enter The Submerged Passage [G The Submerged Passage]

Get The Submerged Passage WP [P The Submerged Passage]
[WP The Coast]

[G The Tidal Island]
Kill Hailrake [QS a1q5 2]
[XP 4]

```

##### Keywords Explanation

| Keyword | Example                   | Description    | Is marked as completed                                                                                                                                  |
| ---     | ---                       | ---            | ---                                                                                                                                                     |
| WP      | [WP The Coast]            | Take Waypoint  | When the Player's current area is equal to "The Coast"                                                                                                  |
| G       | [G The Mud Flats]         | Go To          | When the Player's current area is equal to "The Mud Flats"                                                                                              |
| QS      | [QS a1q1 3]               | "Queast State" | When the quest `a1q1`s state (state id) is equal or lower than the wanted stage "3" [4-3-2-1-0], (the closer to 0 a quest is the further the progress.) |
| QT      | [QT a1q1]                 | Quest Turnin   | When quest is turned in (typically when reward is received)                                                                                             |
| P       | [P The Submerged Passage] | Has Waypoint   | When the Player has the waypoint for the area "The Submerged Passage"                                                                                   |
| XP      | [XP 4]                    | Player level   | When the Player level is equal or greater than "4"                                                                                                      |
