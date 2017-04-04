CREATE PROCEDURE [dbo].[StudentGuardianInsert](@StudentId  INT,
                                              @GuardianId INT,
                                              @Relation   NVARCHAR(50))
AS
     INSERT INTO StudentGuardian
     (StudentId,
      GuardianId,
      Relation,
      IsActive
     )
     VALUES
     (@StudentId,
      @GuardianId,
      @Relation,
      1
     );