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
  * Three roles: User, Contender, Administrator
* Users can see all the tournaments, contenders and their H2H stats, schedule, rankings, etc.
* Users can become contenders, allowing them to sign up for tournaments and plan their tour
* Contenders can sign up for any tournament, but only the top X ranked will get in
* Users that are not logged in can view everything, but can't follow favorite contenders
* Due to the complexity it would introduce, points will not be tracked, only games and sets
* Every tournament happens once a year
* Every tournament has a deadline for registering, after which it generates the main draw
* Only the achievement in the last edition of a tournmanet is counted toward official ranking points
* For performance reasons, the database will not be fully normalized
  * For example, there will be a rankings table, which will be updated periodically 

### Bonus Features (If Time Allows)
* Contenders can schedule a friendly match with other contenders
* Wildcards and qualification tournaments

## Languages & Technologies
* C#
* .NET 6
* Blazor
* SQL Server
* Docker

## Database Model

![TennisTour Database Model](https://raw.githubusercontent.com/OSS-Csharp-Seminar/TennisTour/main/github/images/DbModel.jpg?token=GHSAT0AAAAAACBOXJEOQ7OZKOWV6CR5DMAEZB6F72Q)

## Authors

| Name | GitHub Link |
| --- | --- |
| Toni Kazinoti | [GitHub](https://github.com/tonikazinoti) |
