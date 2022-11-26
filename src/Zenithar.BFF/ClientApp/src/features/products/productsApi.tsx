import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react'

export interface ProductDto {
  id: string;
  name: string;
  price: number;
  previewUrl: string;
}

export interface GetAllProductsResponse {
  items: ProductDto[];
}

const productsApi = createApi({
  reducerPath: 'productsApi',
  baseQuery: fetchBaseQuery({baseUrl: 'api/products'}),
  endpoints: build => ({
    getAllProducts: build.query<GetAllProductsResponse, void>({
      query: () => '/'
    })
  })
})

export default productsApi