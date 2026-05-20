export interface Notification {
Id: number;
  Message: string;
  CreatedDate: Date;
  IsRead: boolean;
  NotificationType: string;
  TargetActionId?: number;
  TargetUserId?:number;
  TargetDepartmentId?:number;
}
