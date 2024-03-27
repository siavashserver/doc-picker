import { GetSpecialitiesRequest } from "@/services/specialities/models/GetSpecialitiesRequest";
import { GetSpecialitiesResponse } from "@/services/specialities/models/GetSpecialitiesResponse";
import { axiosInstance } from "@/utilities/AxiosInstance";

export class SpecialitiesService {
  private static get BaseUrl(): string {
    return `/Specialities`;
  }

  public static async GetSpecialities(
    request: GetSpecialitiesRequest,
  ): Promise<GetSpecialitiesResponse> {
    const url = `${this.BaseUrl}/search`;
    const response = await axiosInstance.post<GetSpecialitiesResponse>(
      url,
      request,
    );
    return response.data;
  }
}
