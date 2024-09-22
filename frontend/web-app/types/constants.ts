export const currencies: {
  [key: string]: { locale: string; name: string; value: number }
} = {
  $: { locale: 'en-US', name: 'USD', value: 0 },
  đ: { locale: 'vi-VN', name: 'VND', value: 1 },
}

export const languages: { [key: string]: number } = {
  English: 0,
  Vietnamese: 1,
}
