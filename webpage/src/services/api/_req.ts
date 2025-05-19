import axios from 'axios'

const instance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL
})

instance.interceptors.request.use(
  (config) => {
    // if (token) {
    //   config.headers['Authorization'] = `Bearer ${token}`
    // }
    return config
  }, 
  (error) => {
    return Promise.reject(error)  
  }
)

instance.interceptors.response.use(
  (response) => response,
  (error) => {
    return Promise.reject(error)
  }
)

export const req = instance
export const httpGet = req.get
export const httpPost = req.post
export const httpPut = req.put
export const httpDel = req.delete