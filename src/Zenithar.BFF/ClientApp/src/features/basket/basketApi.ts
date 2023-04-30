import {createApi} from "@reduxjs/toolkit/query/react"
import {MutationLifecycleApi} from "@reduxjs/toolkit/dist/query/endpointDefinitions"
import {BaseQueryFn, FetchArgs, fetchBaseQuery, FetchBaseQueryError} from "@reduxjs/toolkit/query"
import {ProductDto} from "features/products/productsApi"

export interface BasketDto {
  productBatches: ProductsBatchDto[];
  totalPrice: number;
  totalCount: number;
}

export interface ProductsBatchDto {
  product: ProductDto
  quantity: number;
}

export const basketApi = createApi({
  reducerPath: "basketApi",
  baseQuery: fetchBaseQuery({baseUrl: "/api/basket/"}),
  endpoints: build => ({
    getBasket: build.query<BasketDto, void>({
      query: () => "/"
    }),
    addProduct: build.mutation<BasketDto, string>({
      query: (productId) => ({
        url: `products/${productId}`,
        method: "POST"
      }),
      onQueryStarted: updateBasketQueryCache()
    }),
    removeProduct: build.mutation<BasketDto, string>({
      query: (productId) => ({
        url: `products/${productId}`,
        method: "DELETE"
      }),
      onQueryStarted: updateBasketQueryCache()
    }),
    clear: build.mutation<BasketDto, void>({
      query: () => ({
        url: "clear",
        method: "POST"
      }),
      onQueryStarted: updateBasketQueryCache()
    })
  })
})

type BaseQuery = BaseQueryFn<string | FetchArgs, unknown, FetchBaseQueryError, {}, {}>

function updateBasketQueryCache<QueryArg>() {
  return async function (arg: QueryArg, {
    dispatch,
    queryFulfilled
  }: MutationLifecycleApi<QueryArg, BaseQuery, BasketDto, string>) {
    const {data: basket} = await queryFulfilled
    dispatch(basketApi.util.updateQueryData(
      "getBasket", undefined, () => basket
    ))
  }
}

export default basketApi
