export interface GetSpecialitiesRequest {
  Page: number;
  PageSize: number;
  SpecialityIds: string[];
  Names: string[];
  Descriptions: string[];
}
