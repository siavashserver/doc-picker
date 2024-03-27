import { CreateAccountRequest } from "@/services/accounts/models/CreateAccountRequest";
import { CreateAccountResponse } from "@/services/accounts/models/CreateAccountResponse";
import { GetAccountRequest } from "@/services/accounts/models/GetAccountRequest";
import { GetAccountResponse } from "@/services/accounts/models/GetAccountResponse";
import { UpdateAccountRequest } from "@/services/accounts/models/UpdateAccountRequest";
import { UpdateAccountResponse } from "@/services/accounts/models/UpdateAccountResponse";
import { DeleteAccountRequest } from "@/services/accounts/models/DeleteAccountRequest";
import { DeleteAccountResponse } from "@/services/accounts/models/DeleteAccountResponse";
import { LoginAccountRequest } from "@/services/accounts/models/LoginAccountRequest";
import { LoginAccountResponse } from "@/services/accounts/models/LoginAccountResponse";
import { axiosInstance } from "@/utilities/AxiosInstance";

export class AccountsService {
  private static get BaseUrl(): string {
    return `/Accounts`;
  }

  public static async createAccount(
    request: CreateAccountRequest,
  ): Promise<CreateAccountResponse> {
    const url = `${this.BaseUrl}`;
    const response = await axiosInstance.post<CreateAccountResponse>(
      url,
      request,
    );
    return response.data;
  }

  public static async getAccount(
    request: GetAccountRequest,
  ): Promise<GetAccountResponse> {
    const url = `${this.BaseUrl}/${request.AccountId}`;
    const response = await axiosInstance.get<GetAccountResponse>(url);
    return response.data;
  }

  public static async updateAccount(
    request: UpdateAccountRequest,
  ): Promise<UpdateAccountResponse> {
    const url = `${this.BaseUrl}/${request.AccountId}`;
    const response = await axiosInstance.patch<UpdateAccountResponse>(
      url,
      request,
    );
    return response.data;
  }

  public static async deleteAccount(
    request: DeleteAccountRequest,
  ): Promise<DeleteAccountResponse> {
    const url = `${this.BaseUrl}/${request.AccountId}`;
    const response = await axiosInstance.delete<DeleteAccountResponse>(url);
    return response.data;
  }

  public static async loginAccount(
    request: LoginAccountRequest,
  ): Promise<LoginAccountResponse> {
    const url = `${this.BaseUrl}/login`;
    const response = await axiosInstance.post<LoginAccountResponse>(
      url,
      request,
    );
    return response.data;
  }
}
