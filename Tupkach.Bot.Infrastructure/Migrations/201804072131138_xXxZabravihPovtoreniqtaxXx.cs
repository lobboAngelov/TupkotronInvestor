namespace Tupkach.Bot.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xXxZabravihPovtoreniqtaxXx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workouts", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workouts", "Amount");
        }
    }
}
