use [TOiR_ProfDB]

--ƒобавить проверки где есть даты везде 



--ќборудование
--alter table [dbo].[Equipments]
--	add constraint [UQ_Equipments_InventoryNumber] UNIQUE ([InventoryNumber])  

--alter table [dbo].[Equipments]
--	add constraint [CHK_Equipments_CheckDate] check ([DateCommissioning] < [WorkStartDate] and [WorkStartDate] < GETDATE() and [DateCommissioning] < [DecommissionedDate]) проверка на правильность дат

--alter table [dbo].[Equipments]
--	add constraint [CHK_Equipments_OperationStatusId] check ([OperationalStatusId] >= 8 and [OperationalStatusId] <= 10) проверка нужного стаутса изменить повторно

--alter table [dbo].[Equipments]
--	add constraint [CHK_Equipments_TechnicalStatusId] check ([TechnicalStatusId] >= 1 and [TechnicalStatusId] <= 7) проверк нужного статуса изменить повторно

--«а€вки
--alter table [dbo].[WorkOrders]
--	add constraint [QU_WorkOrders_Number] UNIQUE ([Number])

--alter table [dbo].[WorkOrders]
--	add constraint [CHK_WorkOrders_PriorityId] check ([PriorityId] between 1 and 4)

--alter table [dbo].[WorkOrders]
--	add constraint [CHK_WorckOrders_StatusId] check ([StatusId] >= 11 and [StatusId] <= 17) проверка статуса

--alter table [dbo].[WorkOrders]
--	add constraint [CHK_WorkOrders_CheckDate] check ([ActualEndDate] > [ActualStartDate])


--√рафик обслужевани€
--alter table [dbo].[MaintenanceSchedule]
--	add constraint [CHK_MaintenanceSchedule] check ([PlannedEndDate] > [PlannedStartDate])


--ѕользователи
--alter table [dbo].[Users]
--	add constraint [QU_Users_Email] Unique ([Email])

--alter table [dbo].[Users]
--	add constraint [QU_Users_Phone] unique ([Phone])

--alter table [dbo].[Users]
--	add constraint [CHK_Users_CheckProfession] check (([WorkshopId] is not null and [BrigadeId] is null) or ([WorkshopId] is null and [BrigadeId] is not null)) проверка что бы два пол€ не были запонлнины





--go
--create trigger [TRG_WorkOrders_CreateNumber]
--on [dbo].[WorkOrders]
--after insert
--as
--begin
--	Set nocount on;
	
--	update w
--	set w.[Number] = N'Z'
--		+ N'-'
--		+ CAST(YEAR(GETDATE()) as nvarchar(4))
--		+ N'-'
--		+ RIGHT(N'0000' + CAST(i.[id] as nvarchar(10)), 4)
--		From [dbo].[WorkOrders] w
--		join inserted i on w.[Id] = i.[Id]
--		where w.[Number] is null or w.[Number] = N'';

--end;
--go

--go тригер на проверку статусов у обородувани€ сложна€ верси€
--create trigger [TRG_Equipments_CheckStatus]
--on [dbo].[Equipments]
--after insert, update
--as
--begin
		
--	SET NOCOUNT ON;

--    IF EXISTS (
--        SELECT 1
--        FROM inserted i
--        LEFT JOIN [dbo].[Statuses] os ON os.Id = i.OperationalStatusId
--        LEFT JOIN [dbo].[Statuses] ts ON ts.Id = i.TechnicalStatusId
--        WHERE os.Id IS NULL
--           OR os.EntityTypeId <> 1              
--           OR ts.Id IS NULL
--           OR ts.EntityTypeId <> 2                
--    )
--    BEGIN
--        RAISERROR(
--            N'Ќеверный статус оборудовани€: OperationalStatusId должен иметь EntityTypeId = 1, TechnicalStatusId Ч EntityTypeId = 2.',
--            16, 1
--        );
--        ROLLBACK TRANSACTION;
--    END

--end
--go

--go
--create trigger [TRG_WorkOrders_CheckStatus] тригер что бы статуса за€вки не перескакивала через одну
--on [dbo].[WorkOrders]
--after insert, update
--as
--begin
--	set nocount on;

--	if not exists (select 1 from deleted)
--	begin

--	update wo
--	set wo.StatusId = 11
--	from [dbo].[WorkOrders] wo
--	join inserted i on i.Id = wo.Id
--	return;
--	end;

--	if exists (
--	select 1 
--	from inserted i
--	join deleted d on i.Id = d.Id
--	where ISNULL(i.StatusId, 0) - ISNULL (d.StatusId, 0) not in (0, 1)
--	)
--	begin
--		RAISERROR(N'—татус за€вки не может перескакивать ступень за€вки', 16, 1);
--		ROLLBACK TRANSACTION;
--		return;
--	end;

--end
--go


--go --если оборудование есть в за€вках то его нельз€ будет удалить 16 21 22 завершен и отменен и закрыт
--create or alter trigger [TRG_Equipments_DeletionCheck]
--on [dbo].[Equipments]
--instead of delete
--as
--begin
--	set nocount on;
--	if exists (
--	 select 1
--	 from deleted d
--	 join [dbo].[MaintenanceSchedule] ms on ms.EquipmentId = d.Id
--	 where ISNULL(ms.StatusId, 0) not in (21, 22)
--	)
--	begin
--        raiserror(N'Ќевозможно удалить оборудование: есть активные записи графика “ќ', 16, 1);
--        return;
--    end;

--	 if exists (
--	 select 1
--	 from deleted d
--	 join [dbo].[MaintenanceSchedule] ms on ms.EquipmentId = d.Id
--	 join [dbo].[WorkOrders] wo on wo.MaintenanceScheduleId = ms.Id
--	 where ISNULL(wo.StatusId, 0) != 16
--	)
--	 begin
--        raiserror(N'Ќевозможно удалить оборудование: есть активные за€вки', 16, 1);
--        return;
--    end;

--	delete wo
--    from [dbo].[WorkOrders] wo
--    join [dbo].[MaintenanceSchedule] ms on ms.Id = wo.MaintenanceScheduleId
--    join deleted d on d.Id = ms.EquipmentId;


--    delete ms
--    from [dbo].[MaintenanceSchedule] ms
--    join deleted d on d.Id = ms.EquipmentId;


--    delete e
--    from [dbo].[Equipments] e
--    join deleted d on d.Id = e.Id;

--end;
--go

--go не рабочий
--create trigger [TRG_WorkOrders_DeleteWritten?offMaterials]
--on [dbo].[WorkOrders]
--after delete
--as
--begin

--	delete mu
--	from [dbo].[MaterialUsage] mu
--	join deleted d on mu.WorkOrderId = d.Id;

--end;
--go


--go запрет на удалени€ пользовател€ только на деактивировать
--create trigger [TRG_Users_NoDeleteUser]
--on [dbo].[Users]
--instead of delete
--as
--begin
--	update [dbo].[Users]
--	set IsActive = 0
--	where Id in (select Id from deleted)
--end
--go

--go 
--create trigger [TRG_WorkOrders_StatusUpdateLock]
--after update
--on [dbo].[WorkOrders]