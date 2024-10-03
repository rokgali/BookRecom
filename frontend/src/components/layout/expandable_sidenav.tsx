import { useState } from "react"
import { Home, Settings, User, Mail, ChevronFirst, ChevronLast, MoreHorizontal } from "lucide-react"
import { NavigationPage } from "../../interfaces/navigation_page"
import { useNavigate } from "react-router-dom"

interface ExpandableNavigationProps 
{
  navigations: NavigationPage[]
}


export default function ExpandableSidenav(props: ExpandableNavigationProps) {
  const [expanded, setExpanded] = useState(false)
  const navigate = useNavigate();

  function HandleNavigationClick(url: string) 
  {
    navigate(url);
  }

  return (
    <aside className="h-screen fixed top-0 left-0 z-40 h-screen">
      <nav className={`h-full flex flex-col bg-white border-r shadow-sm ${expanded ? "w-72 max-w-[80%]" : "w-20"} transition-all duration-300`}>
        <div className="p-4 pb-2 flex justify-between items-center">
          <img
            src="/placeholder.svg?height=40&width=40"
            className={`overflow-hidden transition-all ${expanded ? "w-32" : "w-0"}`}
            alt="Logo"
          />
          <button
            onClick={() => setExpanded((curr) => !curr)}
            className="p-1.5 rounded-lg bg-gray-50 hover:bg-gray-100"
          >
            {expanded ? <ChevronFirst /> : <ChevronLast />}
          </button>
        </div>

        <ul className="flex-1 px-3">
          {props.navigations.map((navigation, index) => (
            <li key={index}>
              <button
                onClick={() => HandleNavigationClick(navigation.link)}
                className={`
                  relative flex items-center py-2 px-3 my-1
                  font-medium rounded-md cursor-pointer
                  transition-colors group hover:bg-indigo-50 hover:text-indigo-600
                  ${expanded ? "w-full justify-start" : "w-full justify-center"}
                `}
              >
                <navigation.icon className={`h-5 w-5 ${expanded && "mr-2"}`} />
                <span className={`overflow-hidden transition-all ${expanded ? "w-52 ml-3" : "w-0"}`}>
                  {navigation.name}
                </span>

                {!expanded && (
                  <div
                    className={`
                      absolute left-full rounded-md px-2 py-1 ml-6
                      bg-indigo-100 text-indigo-800 text-sm
                      invisible opacity-20 -translate-x-3 transition-all
                      group-hover:visible group-hover:opacity-100 group-hover:translate-x-0
                    `}
                  >
                    {navigation.name}
                  </div>
                )}
              </button>
            </li>
          ))}
        </ul>

        <div className="border-t flex p-3">
          <div
            className={`
              flex justify-between items-center
              overflow-hidden transition-all
              ${expanded ? "w-52 ml-3" : "w-0"}
            `}
          >
            <div className="leading-4">
              <h4 className="font-semibold">Pvz</h4>
              <span className="text-xs text-gray-600">Pvz@gmail.com</span>
            </div>
          </div>
        </div>
      </nav>
    </aside>
  )
}