export interface UpdateAccountRequest {
  AccountId: number;
  Email: string;
  OldPassword: string;
  NewPassword: string;
}
