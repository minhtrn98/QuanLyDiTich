export type ApiResponse<K extends string, T> = Paging & {
  [key in K]: T[];
};

export interface Paging {
  total: number;
  skip: number;
  limit: number;
}
