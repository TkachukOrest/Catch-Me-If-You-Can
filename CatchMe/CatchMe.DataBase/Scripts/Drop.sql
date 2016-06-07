USE CatchMeDb;

GO

IF EXISTS(select * from sysobjects where name='Passenger' and type='U')
Begin
DROP TABLE [Passenger];
END;

GO

IF EXISTS(select * from sysobjects where name='AddressDetail' and type='U')
Begin
DROP TABLE [AddressDetail];
END;

GO

IF EXISTS(select * from sysobjects where name='MapPoint' and type='U')
Begin
DROP TABLE [MapPoint];
END;

GO

IF EXISTS(select * from sysobjects where name='UserRole' and type='U')
Begin
DROP TABLE [UserRole];
END;

GO

IF EXISTS(select * from sysobjects where name='Role' and type='U')
Begin
DROP TABLE [Role];
END;

GO

IF EXISTS(select * from sysobjects where name='UserProfile' and type='U')
Begin
DROP TABLE [UserProfile];
END;

GO

IF EXISTS(select * from sysobjects where name='Trip' and type='U')
Begin
DROP TABLE [Trip];
END;

GO

IF EXISTS(select * from sysobjects where name='User' and type='U')
Begin
DROP TABLE [User];
END;

GO

IF EXISTS(select * from sysobjects where name='Vehicle' and type='U')
Begin
DROP TABLE [Vehicle];
END;

GO

