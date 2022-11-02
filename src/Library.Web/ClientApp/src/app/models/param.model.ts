export interface ParamBase {
  searchTerm?: string;
  orderBy?: string;
  orderDirection?: 'ASC' | 'DESC';
  skip?: number;
  take?: number;
}
