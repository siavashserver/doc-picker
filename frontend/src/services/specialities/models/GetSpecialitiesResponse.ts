import { GetSpecialitiesResult } from "@/services/specialities/models/GetSpecialitiesResult";

export interface GetSpecialitiesResponse {
  HasNextPage: boolean;
  Specialities: GetSpecialitiesResult[];
}
