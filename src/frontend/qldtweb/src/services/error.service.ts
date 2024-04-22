import { common } from '@/constants'

const { ERROR_TYPE } = common

let instance: GlobalError
let errorInstance: any = {
  type: null,
  detail: null,
  status: null
}

class GlobalError {
  constructor() {
    if (instance) {
      throw new Error('New instance cannot be created!!')
    }

    instance = this
  }

  checkTokenExpiredError = (err: any) => {
    // TODO: Implement the logic to show error if the error is a token expired error

    if (err?.response?.type === ERROR_TYPE.TOKEN_EXPIRED) {
      if (errorInstance && errorInstance?.type === ERROR_TYPE.TOKEN_EXPIRED) {
        return
      }

      errorInstance.type = err?.response?.type
    }
  }
}

let globalErrorInstance = Object.freeze(new GlobalError())

export default globalErrorInstance
