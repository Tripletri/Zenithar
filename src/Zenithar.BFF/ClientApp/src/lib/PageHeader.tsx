import {ReactNode} from 'react'

interface PageHeaderProps {
  children: ReactNode;
}

const PageHeader = ({children}: PageHeaderProps) => {
  return (
    <h1 className="text-center mb-5">{children}</h1>
  )
}

export default PageHeader