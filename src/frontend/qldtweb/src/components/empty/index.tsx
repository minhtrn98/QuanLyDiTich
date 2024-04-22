export const Empty = () => {
  return (
    <div className="flex flex-col items-center justify-center h-full">
      <h1 className="text-3xl font-bold">No products found</h1>
      <p className="text-muted-foreground">Try another search term</p>
    </div>
  );
};
