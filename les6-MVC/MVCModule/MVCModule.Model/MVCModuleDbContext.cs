using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCModule.Models {
    internal class MVCModuleDbContext : IdentityDbContext<IdentityUser> {
        public MVCModuleDbContext()
         : base(GetConnectionString()) {

        }
        private static string GetConnectionString() {
            var dc = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (dc == null)
                //return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MVCModuleDB;Integrated Security=True";
            return @"data source=.\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|aspnetdb.mdf;User Instance=true";
            else
                return dc.ConnectionString;
        }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<TagLine> TagLines { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }

        static MVCModuleDbContext() {
            Database.SetInitializer(new MVCModuleDBInitializer());
            // This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // As it is installed in the GAC, Copy Local does not work. It is required for probing.
            // Fixed "Provider not loaded" error
            _dependency = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }
        private static volatile Type _dependency;
    }
    internal class MVCModuleDBInitializer : IDatabaseInitializer<MVCModuleDbContext> {
        public void InitializeDatabase(MVCModuleDbContext context) {
            var exists = context.Database.Exists();
            if (exists && context.Database.CompatibleWithModel(true)) {
                return;
            }
            if (exists) {
                context.Database.Delete();
            }
            context.Database.Create();
            Seed(context);
        }

        protected void Seed(MVCModuleDbContext context) {
            var now = DateTime.Now.Date;
            context.Doctors.Add(new Doctor() { Id = 1, Name = "Dr. Pieter Philippaerts" });
            context.Doctors.Add(new Doctor() { Id = 2, Name = "Dr. Frédéric Vogels" });
            context.TagLines.Add(new TagLine() { Id = 1, Text = "We make being sick fun" });
            context.TagLines.Add(new TagLine() { Id = 2, Text = "Ask for our amputations savings card!" });
            context.TagLines.Add(new TagLine() { Id = 3, Text = "Writing doctor notes is our business" });
            context.TagLines.Add(new TagLine() { Id = 4, Text = "We keep drugging you until the pain disappears" });
            context.TagLines.Add(new TagLine() { Id = 5, Text = "No medical accidents since mid " + (DateTime.Now.Year - 1) });
            for (int i = 0; i < 50; i++) {
                var date = now.AddDays(RandomHelper.Next(-21, 7));
                context.Appointments.Add(new Appointment() { Patient = Users.Choose(), DoctorId = Doctors.Choose(), Reason = Reasons.Choose(), Date = new DateTime(date.Year, date.Month, date.Day, Hours.Choose(), Minutes.Choose(), 0) });
            }
            context.SaveChanges();

            var um = new MVCModuleUserManager(context);
            um.Create(new IdentityUser("Pieter"), "p");
            um.Create(new IdentityUser("Frederic"), "f");
            um.Create(new IdentityUser("Tuur"), "t");
            um.Create(new IdentityUser("Ilse"), "i");
            um.Create(new IdentityUser("Lore"), "l");
            um.Create(new IdentityUser("Lena"), "l");
            um.Create(new IdentityUser("Wout"), "w");
            um.Create(new IdentityUser("Karel"), "k");
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            rm.Create(new IdentityRole("Doctors"));
            context.SaveChanges();
            um.AddToRole(um.FindByName("Pieter").Id, "Doctors");
            um.AddToRole(um.FindByName("Frederic").Id, "Doctors");
            context.SaveChanges();
        }

        private int[] Doctors = new int[] { 1, 2 };
        private int[] Hours = new int[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
        private int[] Minutes = new int[] { 0, 15, 30, 45 };
        private string[] Users = new string[] { "Tuur", "Ilse", "Lore", "Lena", "Wout", "Karel"};
        private string[] Reasons = new string[] {
            "Vomiting",
            "inability to keep fluids down",
            "Painful swallowing",
            "Coughing that lasts more than 2 weeks",
            "Earache",
            "Diarrhea",
            "bloody stools",
            "Symptoms of dehydration",
            "Digestive problems",
            "A feeling that food is stuck in the throat",
            "heartburn",
            "Frequent regurgitation",
            "Persistent and severe abdominal pain",
            "Persistent nausea",
            "shortness of breath",
            "Unexplained weight loss",
            "Dizziness and about-to-faint feelings",
            "Persistent fatigue",
            "Headache",
            "Severe headache that peaks in intensity within seconds",
            "double vision",
            "Numbness or weakness in the arms",
            "Nausea",
            "galloping heartbeats",
            "Chest pain",
            "Leg problems",
            "Pain in the calves that worsens when walking",
            "Swelling in the left ankle",
            "Severe cramps",
            "Rash",
            "fever",
            "oozing rash",
            "Sinusitis",
            "redness in left eye",
        };
    }
}
