namespace Tupkach.Bot.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TupkacosMigraciqXd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tupkachovs",
                c => new
                    {
                        TupkachovId = c.Int(nullable: false, identity: true),
                        DiscordName = c.String(),
                    })
                .PrimaryKey(t => t.TupkachovId);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        WorkoutId = c.Int(nullable: false, identity: true),
                        TupkachovId = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        WorkoutName = c.String(),
                    })
                .PrimaryKey(t => t.WorkoutId)
                .ForeignKey("dbo.Tupkachovs", t => t.TupkachovId, cascadeDelete: true)
                .Index(t => t.TupkachovId);
            
            CreateTable(
                "dbo.WorkoutTypes",
                c => new
                    {
                        WorkoutId = c.Int(nullable: false, identity: true),
                        WorkoutName = c.String(),
                        KarmaValue = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.WorkoutId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workouts", "TupkachovId", "dbo.Tupkachovs");
            DropIndex("dbo.Workouts", new[] { "TupkachovId" });
            DropTable("dbo.WorkoutTypes");
            DropTable("dbo.Workouts");
            DropTable("dbo.Tupkachovs");
        }
    }
}
