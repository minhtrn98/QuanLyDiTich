import { ElementRef, useEffect, useRef } from "react";
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";
import { cn } from "@/lib/utils";
import { Empty } from "../empty";

interface InfiniteScrollProps {
  children: React.ReactNode;
  fetchNextPage: () => void;
  loader: React.ReactNode;
  isFetching: boolean;
  isLoading?: boolean;
  isEmpty?: boolean;
  hasMore: boolean;
  className?: string;
}

export const InfiniteScroll = ({
  children,
  className,
  fetchNextPage,
  loader,
  isFetching,
  isLoading,
  isEmpty,
  hasMore,
  ...props
}: InfiniteScrollProps) => {
  const scrollRef = useRef<ElementRef<"div">>(null);

  useEffect(() => {
    const observer = new IntersectionObserver(
      (entries) => {
        if (entries[0].isIntersecting) {
          fetchNextPage();
        }
      },
      { threshold: 1 },
    );

    if (scrollRef.current) {
      observer.observe(scrollRef.current);
    }

    return () => {
      if (scrollRef.current) {
        observer.unobserve(scrollRef.current);
      }
    };
  }, [scrollRef]);

  return (
    <ScrollArea
      className={cn(
        "mx-auto h-full w-1/2 whitespace-nowrap rounded-md border",
        className,
      )}
      {...props}
    >
      <div className="flex flex-col m-auto justify-center items-center w-max space-y-2 p-4">
        {children}
        {isLoading && <>{loader}</>}
        {isEmpty && <Empty />}
        <div className="w-full " ref={scrollRef}>
          {isFetching && hasMore && <>{loader}</>}
        </div>
      </div>
      <ScrollBar orientation="horizontal" />
    </ScrollArea>
  );
};
