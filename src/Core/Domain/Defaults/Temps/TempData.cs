using Newtonsoft.Json;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Entities.Training;
using SharedDomain.Entities.Users;

namespace SharedDomain.Defaults.Temps
{
    public static class TempData
    {
        public const string FarmId = "e81dc1f2-4956-4630-b070-9e270713e0a1";
        public const string FarmId2 = "1379cb20-1366-40fc-8142-e57b91b77e95";
        public static Guid AdminId1 = new Guid("8c412e0d-4de2-48bc-9a5c-8482cad10dde");
        public static Guid UserId1 = new Guid("65bc6b9a-6664-4275-a79b-33694251c405");
        public static Guid UserId2 = new Guid("fb80655e-cb2c-4680-9c61-fe6a30c8d95f");
        
        public static Site Farm1 = new()
        {
            Id = new Guid(FarmId),
            Name = "site01",
            IsActive = true,
            SiteCode = "site021.abc",
        };
        public static Site Farm2 = new()
        {
            Id = new Guid(FarmId2),
            Name = "site02",
            IsActive = true,
            SiteCode = "site032.xyz",
        };
        public static Member User1 = new()
        {
            Id = new Guid("b6c81853-6375-4756-b90a-29f624b50fd6"),
            SiteId = new Guid(FarmId)

        };
        public static Member User2 = new()
        {
            Id = new Guid("81e85bae-e3f6-433c-b024-69a33516620a"),
            SiteId = new Guid(FarmId)

        };

        public static FarmSoil Land1 = new()
        {
            Id = new Guid("25589208-3089-465f-a852-83c07b60185b"),
            Name = "Land 01",
            Position = JsonConvert.SerializeObject(new List<PositionPoint>
                        {
                            new(89.21 , 22.13 ),
                            new(29.21 , 63.13 ),
                            new(56.21, 22.13),
                            new(113.21 , 78.13)
                        }),
            Acreage = 100,
            Unit = "m2",
            SiteId = new Guid(FarmId),
            Properties = new ComponentProperty[]
                        {
                            new()
                            {
                                Name = "Heavy metal",
                                Value = 10,
                                Unit = "%"
                            },
                            new()
                            {
                                Name = "pH degree",
                                Value = 10,
                                Unit = "pH"
                            }
                        }
        };
        public static FarmSoil Land2 = new()
        {
            Id = new Guid("9f728694-cfe6-41fe-9801-1cbfa5f96fa7"),
            Name = "Land 02",
            Position = JsonConvert.SerializeObject(new List<PositionPoint>
                        {
                            new(89.21 , 22.13 ),
                            new(29.21 , 63.13 ),
                            new(56.21, 22.13),
                            new(113.21 , 78.13)
                        }),
            Acreage = 100,
            Unit = "m2",
            SiteId = new Guid(FarmId),
            Properties = new ComponentProperty[]
                        {
                            new()
                            {
                                Name = "Heavy metal",
                                Value = 10,
                                Unit = "%"
                            },
                            new()
                            {
                                Name = "pH degree",
                                Value = 10,
                                Unit = "pH"
                            }
                        }
        };
        public static FarmSoil Land3 = new()
        {
            Id = new Guid("6d344e88-b8d7-41bc-878b-dc51364d2d75"),
            Name = "Land 03",
            Position = JsonConvert.SerializeObject(new List<PositionPoint>
                        {
                            new(89.21 , 22.13 ),
                            new(29.21 , 63.13 ),
                            new(56.21, 22.13),
                            new(113.21 , 78.13)
                        }),
            Acreage = 100,
            Unit = "m2",
            SiteId = new Guid(FarmId2),
            Properties = new ComponentProperty[]
                        {
                            new()
                            {
                                Name = "Heavy metal",
                                Value = 10,
                                Unit = "%"
                            },
                            new()
                            {
                                Name = "pH degree",
                                Value = 10,
                                Unit = "pH"
                            }
                        }
        };
        public static FarmSeed Seed1 = new()
        {
            Id = new Guid("559ac487-62ff-493f-8898-9d23c18c8718"),
            Name = "Seed 01",

            Unit = "kg",
            UnitPrice = 200000,
            SiteId = new Guid(FarmId),
            Properties = new ComponentProperty[]
                        {
                            new()
                            {
                                Name = "Heavy metal",
                                Value = 10,
                                Unit = "%"
                            },
                            new()
                            {
                                Name = "pH degree",
                                Value = 10,
                                Unit = "pH"
                            }
                        }
        };
        public static FarmSeed Seed2 = new()
        {
            Id = new Guid("1f98655c-e0f0-4e90-a4c4-94e5a1c69d61"),
            Name = "Seed 02",

            Unit = "kg",
            UnitPrice = 200000,
            SiteId = new Guid(FarmId2),
            Properties = new ComponentProperty[]
                        {
                            new()
                            {
                                Name = "Heavy metal",
                                Value = 10,
                                Unit = "%"
                            },
                            new()
                            {
                                Name = "pH degree",
                                Value = 10,
                                Unit = "pH"
                            }
                        }
        };
        public static CultivationSeason Season1 = new()
        {
            Id = new Guid("9929dad3-61ae-49c7-a398-7995357dca1e"),
            SiteId = new Guid(FarmId),
            Description = "Very well season",
            Title = "Spring",
            StartIn = new DateTime(2024, 2, 16, 12, 0, 0),
            EndIn = new DateTime(2024, 4, 16, 13, 0, 0),
        };
        public static CultivationSeason Season2 = new()
        {
            Id = new Guid("33177d84-7368-40d4-882d-8d7fc00ff32b"),
            SiteId = new Guid(FarmId),
            Description = "Very well season",
            Title = "Fall",
            StartIn = new DateTime(2024, 5, 16, 12, 0, 0),
            EndIn = new DateTime(2024, 7, 16, 13, 0, 0),
        };

        public static TrainingContent[] TrainingContents = new TrainingContent[]{
            new()
            {
                Id= new Guid("cc0f8c70-b4f7-4cf7-9de5-83c4f5c8396f"),
                Content = "This is 'How to start season' content.",
                Title = "How to start season",
                SiteId = Farm1.Id
            },
            new()
            {
                Id= new Guid("79fff957-9464-4745-8150-73faca8d55d5"),
                Content = "This is 'Expert recommend' content.",
                Title = "Expert recommend",
                SiteId = Farm1.Id
            }
        };

        public static ExpertInfo[] Experts = new ExpertInfo[]{
            new()
            {
                Id= new Guid("b7119c1e-f585-4a9c-b22d-f8441d350963"),
                ExpertField = "All",
                FullName = "Expert 01",
                Description = @"It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. 
                            The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as 
                            opposed to using 'Content here, content here', making it look like readable English. 
                            Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model 
                            text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. 
                            Various versions have evolved over the years
                                , sometimes by accident, sometimes on purpose (injected humour and the like).",
                SiteId = Farm1.Id,
                Certificates = new()
                {
                    new()
                    {
                        Name = "A",
                        Reference = "/url"
                    },
                    new()
                    {
                        Name = "B"
                    }
                }

            },
            new()
            {
                Id= new Guid("7a7a8916-c767-4ad6-b98c-10775ba4a86c"),
                ExpertField = "All",
                FullName = "Expert 02",
                SiteId = Farm1.Id
            }
        };
        public static Guid Activity4Id = new Guid("9358ac80-ae08-43b9-a672-9009ab059e37");
        public static Guid TrainingDetail1Id = new Guid("157b3aaf-3b49-4d52-9d4b-5702a76c5b6e");

        public static TrainingDetail[] TrainingDetails = new TrainingDetail[]{
            new()
            {
                Id= new Guid("f69485b0-ddcc-45f3-b4c3-79d8f90be953"),
                ActivityId = Activity4Id,
                AdditionType = AdditionType.Training,
                ExpertId = Experts[0].Id,
                Title = "trainig 01",
                ContentId = TrainingContents[0].Id,
                Description = "123 content",

            }
        };

        public static Activity Activity1 = new()
        {
            Id = new Guid("b6c81853-6375-4756-b90a-29f624b50fd6"),
            SiteId = new Guid(FarmId),
            SeasonId = Season1.Id,
            Notes = new() {
                new() {
                    Name = "Introduction",
                    Value = "Very well season"
                }
            },
            Title = "Go to field",
            LocationId = Land1.Id,
            Participants = new List<ActivityParticipant>()
            {
                new()
                {
                    ParticipantId = AdminId1,
                    Role = ActivityRole.Inspector.ToString(),


                },
                new()
                {
                    ParticipantId = UserId1,
                    Role = ActivityRole.Assignee.ToString(),

                }
            },
            StartIn = new DateTime(2024, 3, 16, 12, 0, 0),
            EndIn = new DateTime(2024, 3, 16, 13, 0, 0),
        };
        public static Activity Activity2 = new()
        {
            Id = new Guid("6b200391-41d4-46d6-aa97-191b3ea5d43b"),
            SiteId = new Guid(FarmId),
            SeasonId = Season1.Id,
            Notes = new() {
                new() {
                    Name = "Introduction",
                    Value = "Very well season"
                }
            },
            Title = "Hydrat water",
            LocationId = Land1.Id,
            Participants = new List<ActivityParticipant>()
            {
                new()
                {
                    ParticipantId = AdminId1,
                    Role = ActivityRole.Inspector.ToString(),


                },
                new()
                {
                    ParticipantId = UserId1,
                    Role = ActivityRole.Assignee.ToString(),

                }
            },
            StartIn = new DateTime(2024, 3, 17, 14, 0, 0),
            EndIn = new DateTime(2024, 3, 17, 15, 0, 0),
        };

        public static Activity Activity3 = new()
        {
            Id = new Guid("66db7e18-7db3-4c7e-9ca4-c83a0501c9a0"),
            SiteId = new Guid(FarmId),
            SeasonId = Season1.Id,
            Notes = new() {
                new() {
                    Name = "Introduction",
                    Value = "Very well season"
                }
            },
            Title = "Use Seed 01",
            LocationId = Land1.Id,
            Participants = new List<ActivityParticipant>()
            {
                new()
                {
                    ParticipantId = AdminId1,
                    Role = ActivityRole.Inspector.ToString(),


                },
                new()
                {
                    ParticipantId = UserId2,
                    Role = ActivityRole.Assignee.ToString(),

                }
            },
            Addtions = new List<AdditionOfActivity>()
            {
                new UsingDetail()
                {
                    AdditionType = AdditionType.Use,
                    ComponentId = Seed1.Id,
                    UseValue = 10,
                    Unit = "kg",

                }
            },
            StartIn = new DateTime(2024, 3, 18, 8, 0, 0),
            EndIn = new DateTime(2024, 3, 18, 9, 0, 0),
        };

        public static Activity Activity4 = new()
        {
            Id = Activity4Id,
            SiteId = Farm1.Id,
            SeasonId = Season1.Id,
            Notes = new() {
                new() {
                    Name = "Introduction",
                    Value = "Very well season"
                }
            },
            Title = "Training 01",
            Participants = new List<ActivityParticipant>()
            {
                new()
                {
                    ParticipantId = AdminId1,
                    Role = ActivityRole.Inspector.ToString(),


                },
                new()
                {
                    ParticipantId = UserId2,
                    Role = ActivityRole.Assignee.ToString(),

                }
            },
            Addtions = new List<AdditionOfActivity>()
            {
                new TrainingDetail()
                {
                    Id = TrainingDetail1Id,
                    AdditionType = AdditionType.Training,
                    ExpertId = Experts[0].Id,
                    Title = "trainig 01",
                    ContentId = TrainingContents[0].Id,
                    Description = "123 content",
                    
                }
            },
            StartIn = new DateTime(2024, 3, 20, 8, 0, 0),
            EndIn = new DateTime(2024, 3, 20, 9, 0, 0),
        };

        public static FarmProduct[] Products = new FarmProduct[]
        {
            new()
            {
                Id = new Guid("848edc8e-23e6-4c52-a055-7389e0c377f9"),
                Name = Seed1.Name,
                Quantity = 0,
                Unit = "kg"
            },
            new()
            {
                Id = new Guid("a7b33f43-2de1-4c73-af61-d3fd9470dc91"),
                Name = Seed2.Name,
                Quantity = 0,
                Unit = "kg"
            }
        };

        public static ProductionDetail[] Productions = new ProductionDetail[]
        {
            new()
            {
                Id = new Guid("686f3827-d44a-4803-8725-38b9732bdb7c"),
                SeedId = Seed1.Id,
                LandId = Land1.Id,
                SeasonId = Season1.Id,
                Unit = "kg",
                TraceItem = new(null, TraceType.QRCODE,"asdasdasdsadsadsadsadsadsdsadsadssdsadsad"),
                SiteId = new Guid(FarmId),
            },
            new()
            {
                Id = new Guid("715de149-cbaa-4078-81b1-5960708cbd87"),
                SeedId = Seed1.Id,
                LandId = Land2.Id,
                SeasonId = Season1.Id,
                Unit = "kg",
                SiteId = new Guid(FarmId),

            },
            new()
            {
                Id = new Guid("ba28147e-dfa2-4854-893f-e2672f9bb4ba"),
                SeedId = Seed1.Id,
                LandId = Land1.Id,
                SeasonId = Season2.Id,
                Unit = "kg",
                SiteId = new Guid(FarmId),

            },

        };

        
    }
}
