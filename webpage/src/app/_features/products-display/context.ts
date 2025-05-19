import { createContext, use } from "react";

export const createProductsDisplayContext = <T>() => {
  const Context = createContext<T | undefined>(undefined)

  const useProductsContext = () => {
    return use(Context)
  }

  return { Context, useProductsContext }
}