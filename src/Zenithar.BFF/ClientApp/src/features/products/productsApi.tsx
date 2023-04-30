import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react"

export interface ProductDto {
  id: string;
  name: string;
  price: number;
  previewUrl: string;
}

export interface GetAllProductsResponse {
  items: ProductDto[];
}

export interface CreateProductRequest {
  name: string;
  price: number;
  image: File;
}

export interface UpdateProductRequest {
  id: string;
  name: string;
  price: number;
  image?: File;
}

export interface RemoveProductRequest {
  id: string;
}

const productsApi = createApi({
  reducerPath: "productsApi",
  baseQuery: fetchBaseQuery({baseUrl: "/api/products"}),
  tagTypes: ["Products"],
  endpoints: build => ({
    getAllProducts: build.query<GetAllProductsResponse, void>({
      query: () => "/",
      providesTags: ["Products"]
    }),

    createProduct: build.mutation<ProductDto, CreateProductRequest>({
      query: ({name, price, image}) => {
        const formData = new FormData();
        formData.set("Name", name);
        formData.set("Price", price.toString());
        formData.set("Image", image);

        return {
          url: "/",
          method: "POST",
          body: formData
        }
      },
      invalidatesTags: ["Products"]
    }),

    updateProduct: build.mutation<ProductDto, UpdateProductRequest>({
      query: ({id, name, price, image}) => {
        const formData = new FormData();
        formData.set("Name", name);
        formData.set("Price", price.toString());
        image && formData.set("Image", image);

        return {
          url: id,
          method: "PUT",
          body: formData
        }
      },
      invalidatesTags: ["Products"]
    }),

    removeProduct: build.mutation<void, RemoveProductRequest>({
      query: ({id}) => ({
        url: `/${id}`,
        method: "DELETE"
      }),
      invalidatesTags: ["Products"]
    })
  })
})

export default productsApi