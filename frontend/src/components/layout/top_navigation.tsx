import { useState } from "react"
import { Bell, Search, Menu, X } from "lucide-react"
import { NavigationPage } from "../../interfaces/navigation_page"
import { useLocation } from "react-router-dom"

interface TopNavigationProps 
{
  navigations: NavigationPage[]
}

export default function TopNavigation(props: TopNavigationProps) {
  const location = useLocation();

  function UnderLineNav(path: string): boolean
  {
    console.log(location.pathname);

    if(location.pathname === path)
      return true;

    return false;
  }

  return (
    <nav className="fixed top-0 left-0 right-0 z-50 bg-white border-b shadow-sm">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between h-16">
          <div className="flex">
            <div className="flex-shrink-0 flex items-center">
              <img
                className="h-8 w-auto"
                src="/placeholder.svg?height=32&width=32"
                alt="Logo"
              />
            </div>
            <div className="hidden sm:ml-6 sm:flex sm:space-x-8">
              {props.navigations.map((navigation, index) => (
                <a 
                  key={index} 
                  href={navigation.link}
                  className={`${UnderLineNav(navigation.link) ? 'border-indigo-500' : ''} text-gray-900 inline-flex items-center px-1 pt-1 border-b-2 text-sm font-medium`}
                >
                  {navigation.name}
                </a>
              ))}
            </div>
          </div>
        </div>
      </div>
    </nav>
  )
}