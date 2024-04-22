import { z } from 'zod'

const loginSchema = z.object({
  username: z.string().min(2).max(50)
})

export default {
  loginSchema
}
