import React, { DetailedHTMLProps, FormEvent, FormHTMLAttributes } from 'react';
import { FieldValues, UseFormReturn } from 'react-hook-form';
import { Form } from '@/components/ui/form';

type FormControlProps<
  T extends FieldValues,
  TContext = any,
  TTransformedValues extends FieldValues | undefined = undefined,
> = DetailedHTMLProps<FormHTMLAttributes<HTMLFormElement>, HTMLFormElement> & {
  form: UseFormReturn<T, TContext, TTransformedValues>;
  children: React.ReactNode;
  onFinish: (data: T) => void;
};

const FormControl = <T extends FieldValues = FieldValues>(props: FormControlProps<T>) => {
  const { form, children, onFinish, ...rest } = props;
  const handleFinish = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    form.handleSubmit(onFinish)();
  };

  return (
    <Form {...form}>
      <form {...rest} onSubmit={handleFinish}>
        {children}
      </form>
    </Form>
  );
};

export default FormControl;
