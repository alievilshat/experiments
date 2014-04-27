<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select distinct dc.id as companyid, dc.targetdirectory, dc.pollingdirectory, dc.traceurl, con.*, ds.businessid, ds.clientnr, ds.ibannumber, sb.stockid from str_deliverycompany dc left join sys_deliveryconnection con on con.id = dc.id inner join str_deliveryservice ds on ds.deliverycompanyid = dc.id inner join str_stockbusiness sb on sb.businessid = ds.businessid ')">
          <stockdeliverycompany>
            <stockdeliverycompanyid m:left="-40" m:top="171">pk:stockdeliverycompany(<xsl:value-of select="companyid" />-<xsl:value-of select="businessid" />-<xsl:value-of select="stockid" />)</stockdeliverycompanyid>
            <targetdirectory m:left="-40" m:top="355">
              <xsl:value-of select="targetdirectory" />
            </targetdirectory>
            <pollingdirectory m:left="82" m:top="370">
              <xsl:value-of select="pollingdirectory" />
            </pollingdirectory>
            <tracktraceurl m:left="87" m:top="440">
              <xsl:value-of select="traceurl" />
            </tracktraceurl>
            <server m:left="84" m:top="185">
              <xsl:value-of select="server" />
            </server>
            <database m:left="-40" m:top="202">
              <xsl:value-of select="database" />
            </database>
            <user m:left="82" m:top="217">
              <xsl:value-of select="user" />
            </user>
            <password m:left="-42" m:top="234">
              <xsl:value-of select="password" />
            </password>
            <language m:left="81" m:top="249">
              <xsl:value-of select="language" />
            </language>
            <shippingurl m:left="-42" m:top="265">
              <xsl:value-of select="shippingurl" />
            </shippingurl>
            <reporturl m:left="81" m:top="280">
              <xsl:value-of select="reporturl" />
            </reporturl>
            <cancelurl m:left="-41" m:top="295">
              <xsl:value-of select="cancelurl" />
            </cancelurl>
            <deliverycompanyid m:left="81" m:top="310">fk:company(delivery-<xsl:value-of select="companyid" />)</deliverycompanyid>
            <deliveryserviceclientnumber m:left="-41" m:top="324">
              <xsl:value-of select="clientnr" />
            </deliveryserviceclientnumber>
            <deliveryserviceiban m:left="81" m:top="339">
              <xsl:value-of select="ibannumber" />
            </deliveryserviceiban>
            <stockid m:left="-41" m:top="385">
              <xsl:value-of select="stockid" />
            </stockid>
            <businessid m:left="81" m:top="402">
              <xsl:value-of select="businessid" />
            </businessid>
          </stockdeliverycompany>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>