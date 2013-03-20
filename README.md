MVC4-Authentication-Authorization
=================================

A simple demo app for authentication and authorization in MVC 4/ASP.NET/C#

To try out the login information, check out the example data below

## User Seed Data

If you are curious about the user seed data, please check out the Configuration.cs file, located in TestApp/TestApp/Migrations

### User Name/Passwords

The seed data was stored as an xml file, and it is expected to be like {username,password}. Obviously, the example data is not secure. It would be best to force users to change passwords on first log in, but this is for demos sake.

All passwords should be salted and hashed correctly through the SimpleMembershipProvider function provided by ASP.NET, which uses the Crypto.HashPassword method. As stated by the Microsoft documentation: 

    "The password hash is generated with the RFC 2898 algorithm using a 128-bit salt, a 256-bit subkey, and 1000 iterations. The format of the generated hash bytestream is {0x00, salt, subkey}, which is base-64 encoded before it is returned."

References:

- http://msdn.microsoft.com/en-us/library/system.web.helpers.crypto.hashpassword(v=vs.111).aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-1
- http://aspnetwebstack.codeplex.com/SourceControl/changeset/view/6f36424e390e#src/WebMatrix.WebData/SimpleMembershipProvider.cs

The example seed data is as follows:

- {csUndergrad; csUndergrad}
- {seUndergrad, seUndergrad}
- {isUndergrad, isUndergrad}
- {advisor, advisor}
- {admin, admin}
- {msGrad, msGrad}
- {mseGrad, mseGrad}
- {phdGrad, phdGrad}
