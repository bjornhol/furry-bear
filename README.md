[furry-bear](http://furry-bear.azurewebsites.net/)
==========

[![Build status](https://ci.appveyor.com/api/projects/status/92fj6lb1j7ptu820?svg=true)](https://ci.appveyor.com/project/bjornhol/furry-bear)

This is where I try out stuff. I hope it will end up in a tool for publishing messages internally in a company. Something that can replace mass email. Here are some of the technologies used:

* ASP.NET MVC
* IOC with [Castle Windsor](http://docs.castleproject.org/Windsor.MainPage.ashx)
* Error logging with [Elmah](https://code.google.com/p/elmah/)
* Backed with [RavenDB](http://ravendb.net/)
* [Memcached](http://memcached.org/)

To run
------

You need to add RavenDB connection string and Memcached endpoint/credentials.