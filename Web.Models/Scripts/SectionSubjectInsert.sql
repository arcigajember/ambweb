USE [AMBData]
GO
/****** Object:  StoredProcedure [dbo].[SectionSubjectInsert]    Script Date: 10/11/2016 4:02:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SectionSubjectInsert](
    @SectionId int,
    @SubjectId int
)
AS
    INSERT dbo.SectionSubject
    (
        --SectionSubjectId - this column value is auto-generated
        SectionId,
        SubjectId
    )
    VALUES
    (
        -- SectionSubjectId - int
        @SectionId, -- SectionId - int
        @SubjectId -- SubjectId - int
    )
