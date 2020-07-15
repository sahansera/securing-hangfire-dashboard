# Securing Hangfire Dashboard in ASP.NET Core with a Custom Auth Policy
Securing Hangfire Dashboard in ASP.NET Core 3.1 with Endpoint Routing in Production.

![](https://sahansera.dev/static/3bfc9ed45e360dfd36a1d276cd09c606/74d4a/hangfire-aspnetcore-3.png)

## Introduction
By default, if you try to access your Hangfire dashboard, it will work perfectly fine on your localhost because local requests are allowed. However, things can get a bit tricky when you want to secure your Dashboard.

This repo targets on providing a starting point to securing your Hangfire dashboard in production with .NET Core 3.1

With this approach, you can have a nicely decoupled way of protecting your Hangfire dashboard route. You can also move the authorisation logic to a custom extension method and inject your custom services as opposed to using a Hangfire’s authorisation filter.

## Related Blog Post
https://sahansera.dev/securing-hangfire-dashboard-with-endpoint-routing-auth-policy-aspnetcore/

## Questions? Bugs? Suggestions for Improvement?
Having any issues or troubles getting started? [Get in touch with me](https://sahansera.dev/contact/) 

## Support
Has this Project helped you learn something new? or helped you at work? Do Consider Supporting.

<a href="https://www.buymeacoffee.com/sahan" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" width="200"  ></a>

## Share it!
Please share this Repository within your developer community, if you think that this would a difference! Cheers.

## About the Author
### Sahan Serasinghe
- Blogs at [sahansera.dev](https://sahansera.dev/)
- Twitter - [sahan91](https://www.twitter.com/sahan91)
- Linkedin - [Sahan Serasinghe](https://www.linkedin.com/in/sahanserasinghe/)
