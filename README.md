# TennisTour - Tennis Singles Tournaments Organization

## Table of Contents

* [General Info](#general-info)
* [Languages & Technologies](#languages--technologies)
* [Features](#features)
  * [Key Features](#key-features)
  * [Bonus Features](#bonus-features-if-time-allows)
* [Database Model](#database-model)
* [Authors](#authors)

## General Info

TennisTour is like ATP/WTA Tour, except it's not actually real

## Features

### Key Features
* User System
  * Three roles: User, Contender, Admin
* Users can see all the tournaments, contenders and their H2H stats, schedule, rankings, favorites, etc.
* Users can become contenders, allowing them to sign up for tournaments and plan their tour
* Contenders can sign up for any tournament, but only the top X ranked will get in
* Users that are not logged in can view everything, but can't follow favorite contenders
* Due to the complexity it would introduce, points will not be tracked, only games and sets
* Every tournament can have multiple instances
* Every tournament needs a certain amount of registrations before the draw can be generated
* Only the achievement in the last edition of a tournmanet is counted toward official ranking points
* For performance reasons, the database will not be fully normalized
  * For example, there will be a rankings table, which will be updated periodically
* Contenders have to report the result after playing a match and the opponent needs to then confirm it
* System supports cases where a player gets injured and can't continue

### Bonus Features (If Time Allows)
* Contenders can schedule a friendly match with other contenders
* Wildcards and qualification tournaments
* Admins can settle cases where two contenders can't seem to confirm the result of the match
* Umpire role

## Languages & Technologies
* C#
* .NET 6
* Blazor
* SQL Server
* Docker

## Database Model

![TennisTour Database Model](https://github.com/OSS-Csharp-Seminar/TennisTour/blob/d592c572ad5141fe73dcbe46d1fd3b91f8845ab4/github/images/DbModel.jpg)

## Authors

| Name | GitHub Link |
| --- | --- |
| Toni Kazinoti | [GitHub](https://github.com/tonikazinoti) |
| Antonio Šabić | [GitHub](https://github.com/ansabic) |
