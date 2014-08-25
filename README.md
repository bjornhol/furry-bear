[furry-bear](http://furry-bear.azurewebsites.net/)
==========

This is where I try out stuff. I hope it will end up in a tool for publishing messages internally in a company. Something that can replace mass email.

Currently I've based the solution on 

* ASP.NET MVC
* IOC with [Castle Windsor](http://docs.castleproject.org/Windsor.MainPage.ashx)
* Error logging with [Elmah](https://code.google.com/p/elmah/)
* Backed with [RavenDB](http://ravendb.net/)
* [Memcached](http://memcached.org/)

Currently I'm trying to figure out how to do authentication and make company "spaces", like on Yammer.

To run
------

You need to add RavenDB connection string and Memcached endpoint/credentials.