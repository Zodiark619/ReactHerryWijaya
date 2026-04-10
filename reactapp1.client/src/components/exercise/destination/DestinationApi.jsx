import {fetchBaseQuery,createApi } from "@reduxjs/toolkit/query/react"


export const destinationAPI = createApi({
    reducerPath: "apidestination",
    baseQuery: fetchBaseQuery({ baseUrl: "http://localhost:3000/" }),
    tagTypes: ["Destinations"],
    endpoints: (builder) => ({
        getAllDestination: builder.query({
            query: () => ({
                url: "destination",
                method: 'GET',
                params: {}
            }),
            transformResponse:(res)=>res.sort((a,b)=>b.id-a.id),
            providesTags:["Destinations"]
        }),
        //getAllDestinationById: builder.query({
        //    query: (id) => `destination/${id}`,
        //    providesTags: (result, error, id) => [{ type: 'Destination' ,id}]

            
        //}),
        addDestination: builder.mutation(({
            query: (destination) => ({
                url: "destination",
                method: "POST",
                body: destination
            }),
            invalidatesTags: ["Destinations"]
        })),
        updateDestination: builder.mutation(({
            query: (destination) => ({
                url: `destination/${destination.id}`,
                method: "PUT",
                body: destination
            }),
            invalidatesTags: ["Destinations"]

        })),
        deleteDestination: builder.mutation(({
            query: ({id}) => ({
                url: `destination/${id}`,
                method: "DELETE", 
            }),
            invalidatesTags: ["Destinations"]

           // providesTags: (result, error, id) => [{ type: 'Destination', id }]

        })),
    })
})

export const { useGetAllDestinationQuery,useAddDestinationMutation,useUpdateDestinationMutation,useDeleteDestinationMutation } = destinationAPI