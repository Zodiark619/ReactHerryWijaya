import {fetchBaseQuery,createApi } from "@reduxjs/toolkit/query/react"


export const randomdestinationAPI = createApi({
    reducerPath: "apirandomdestination",
    baseQuery: fetchBaseQuery({ baseUrl: "https://randomuser.me/api/" }),
    tagTypes: ["randomDestinations"],

    endpoints: (builder) => ({
        getRandomDestination: builder.query({
            query: () => ({
                url:"",
                method: 'GET',
                params: {}
            }),
            transformResponse: (res) => {
                const user = res.results[0]
                return {
                    city: user.location.city,
                    country: user.location.country
                }
            }, 
            invalidatesTags: ["randomDestinations"]

        }),
    
    })
})

export const { useGetRandomDestinationQuery } = randomdestinationAPI