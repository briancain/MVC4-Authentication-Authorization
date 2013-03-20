namespace TestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Reflection;
    using System.Web.Security;
    using System.Xml;
    using TestApp.Models;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<TestApp.Models.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TestApp.Models.UsersContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            WebSecurity.InitializeDatabaseConnection(
               "DefaultConnection",
               "UserProfile",
               "UserId",
               "UserName", autoCreateTables: true);

            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!Roles.RoleExists("Students"))
                Roles.CreateRole("Students");

            if (!Roles.RoleExists("Advisors"))
                Roles.CreateRole("Advisors");

            //String resourceName = "TestApp.SeedData.";
            // Relative path not working for some reason, but direct path works
            String path = @"C:\Users\brian\Dropbox\School\CIS726\Projects\TestApp\TestApp\SeedData\ksu_userdata.xml";
            XmlTextReader reader = new XmlTextReader(path);
            //XmlTextReader reader = new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName + "ksu_userdata.xml"));
            //Course course = null;
            String property = null;
            String username = null;
            String password = null;
            Boolean done = false;
            while (reader.Read() && !done)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals("htmlparse.UserList"))
                        {
                            //udlist = new UserData();
                            username = null;
                            password = null;
                        }
                        else
                        {
                            property = reader.Name;
                        }
                        break;
                    case XmlNodeType.Text:
                        if (property.Equals("username"))
                        {
                            username = reader.Value;
                            //udlist.Username = reader.Value;
                        }
                        else if (property.Equals("password"))
                        {
                            password = reader.Value;
                            //udlist.Password = reader.Value;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name.Equals("htmlparse.UserList"))
                        {
                            try
                            {
                                //context.UserProfiles.AddOrUpdate(i => i.ID, udlist);
                                if (!WebSecurity.UserExists(username))
                                    WebSecurity.CreateUserAndAccount(
                                        username,
                                        password);

                                switch (username)
                                {
                                    case "admin":
                                        if (!Roles.GetRolesForUser(username).Contains("Administrator"))
                                            Roles.AddUsersToRoles(new[] { username }, new[] { "Administrator" });
                                        break;
                                    case "advisor":
                                        if (!Roles.GetRolesForUser(username).Contains("Advisors"))
                                            Roles.AddUsersToRoles(new[] { username }, new[] { "Advisors"});
                                        break;
                                    default:
                                        if (!Roles.GetRolesForUser(username).Contains("Students"))
                                            Roles.AddUsersToRoles(new[] { username }, new[] { "Students" });
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        break;
                }
            }

            reader.Close();
        }
    }
}
